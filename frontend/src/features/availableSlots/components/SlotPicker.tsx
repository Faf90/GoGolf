import type { Slot } from "../types";

interface Props {
  slots: Slot[];
  selectedSlot: Slot | null;
  onSelect: (slot: Slot) => void;
}

function formatTime(localTime: string) {
  return localTime.slice(0, 5); // "08:00:00" -> "08:00"
}

export default function SlotPicker({ slots, selectedSlot, onSelect }: Props) {
  return (
    <div className="flex flex-wrap gap-2">
      {slots.map((slot) => {
        const isSelected = slot.startsAt === selectedSlot?.startsAt;

        return (
          <button
            key={slot.startsAt}
            type="button"
            onClick={() => onSelect(slot)}
            className={`rounded-lg px-4 py-2.5 text-sm font-semibold ring-1 transition ${
              isSelected
                ? "bg-brand text-white ring-brand"
                : "bg-white text-slate-700 ring-slate-200 hover:ring-brand-hover"
            }`}
          >
            {formatTime(slot.localTime)}
          </button>
        );
      })}
    </div>
  );
}
