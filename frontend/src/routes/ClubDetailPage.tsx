// src/routes/ClubDetailPage.tsx
import { useParams, Link } from "react-router-dom";
import { useEffect, useState } from "react";
import { useClub } from "../features/clubs/hooks/useClub";
import { useAvailableSlots } from "../features/availableSlots/hooks/useAvailableSlots";
import { addDays, dayOfWeek } from "../lib/dates";
import DateStrip from "../features/clubs/components/DateStrip";
import SlotPicker from "../features/availableSlots/components/SlotPicker";
import type { Slot } from "../features/availableSlots/types";
import PlayerPicker from "../features/availableSlots/components/PlayerPicker";
import BayPicker from "../features/availableSlots/components/BayPicker";

export default function ClubDetailPage() {
  const { clubId } = useParams<{ clubId: string }>();
  const { club, status, error } = useClub(clubId);

  const [selectedDate, setSelectedDate] = useState<string | null>(null);
  const [selectedSlot, setSelectedSlot] = useState<Slot | null>(null);
  const [selectedPlayerCount, setSelectedPlayerCount] = useState<number | null>(
    null,
  );
  const [selectedBayId, setSelectedBayId] = useState<number | null>(null);

  const { data: availability, status: slotsStatus } = useAvailableSlots(
    clubId,
    selectedDate,
  );

  // default to the first open day
  useEffect(() => {
    if (!club || selectedDate) return;
    const openDays = new Set(club.operatingHours.map((h) => h.dayOfWeek));
    let candidate = club.bookableFrom;
    while (
      candidate <= club.bookableUntil &&
      !openDays.has(dayOfWeek(candidate))
    ) {
      candidate = addDays(candidate, 1);
    }
    setSelectedDate(candidate);
  }, [club, selectedDate]);

  // select the first slot whenever new availability arrives; clears a stale selection
  useEffect(() => {
    setSelectedSlot(availability?.slots[0] ?? null);
  }, [availability]);

  // select the first player count whenever new availability arrives; clears a stale selection
  useEffect(() => {
    if (!availability || selectedPlayerCount !== null) return;

    setSelectedPlayerCount(availability.playerRates[0]?.playerCount ?? null);
  }, [availability, selectedPlayerCount]);

  // clear the bay whenever the slot changes — bays are per-slot
  useEffect(() => {
    setSelectedBayId(null);
  }, [selectedSlot]);

  if (status === "loading") return <p>Loading…</p>;

  if (status === "error" || !club) {
    return (
      <div>
        <p>
          {error?.status === 404 ? "Club not found." : "Something went wrong."}
        </p>
        <Link to="/">Back to clubs</Link>
      </div>
    );
  }

  return (
    <div className="space-y-6">
      <div>
        <h1 className="mt-2 text-2xl font-bold text-slate-900">{club.name}</h1>
        <p className="text-slate-500">{club.address}</p>
      </div>

      {selectedDate && (
        <DateStrip
          bookableFrom={club.bookableFrom}
          bookableUntil={club.bookableUntil}
          operatingHours={club.operatingHours}
          selectedDate={selectedDate}
          onSelect={setSelectedDate}
        />
      )}

      <div>
        <h2 className="mb-3 text-sm font-semibold text-slate-900">Time</h2>

        {slotsStatus === "loading" && (
          <p className="text-slate-500">Loading times…</p>
        )}

        {slotsStatus === "error" && (
          <p className="text-red-600">Couldn't load available times.</p>
        )}

        {slotsStatus === "ready" &&
          availability &&
          (!availability.isOpen ? (
            <p className="text-slate-500">Closed on this day.</p>
          ) : availability.slots.length === 0 ? (
            <p className="text-slate-500">No times left for this day.</p>
          ) : (
            <SlotPicker
              slots={availability.slots}
              selectedSlot={selectedSlot}
              onSelect={setSelectedSlot}
            />
          ))}
      </div>

      {slotsStatus === "ready" &&
        availability &&
        availability.isOpen &&
        availability.slots.length > 0 && (
          <div>
            <h2 className="mb-3 text-sm font-semibold text-slate-900">
              Players
            </h2>
            <PlayerPicker
              rates={availability.playerRates}
              selectedCount={selectedPlayerCount}
              onSelect={setSelectedPlayerCount}
            />
          </div>
        )}

      {selectedSlot && (
        <div>
          <h2 className="mb-3 text-sm font-semibold text-slate-900">Bay</h2>
          <BayPicker
            bays={selectedSlot.bays}
            selectedBayId={selectedBayId}
            onSelect={setSelectedBayId}
          />
        </div>
      )}
    </div>
  );
}
