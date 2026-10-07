// src/features/clubs/components/DateStrip.tsx
import { useMemo, useRef, useEffect } from "react";
import { addDays, dayOfWeek, formatDayLabel } from "../../../lib/dates";
import type { OperatingHours } from "../types";

interface Props {
  bookableFrom: string;
  bookableUntil: string;
  operatingHours: OperatingHours[];
  selectedDate: string;
  onSelect: (date: string) => void;
}

export default function DateStrip({
  bookableFrom,
  bookableUntil,
  operatingHours,
  selectedDate,
  onSelect,
}: Props) {
  const openDays = useMemo(
    () => new Set(operatingHours.map((h) => h.dayOfWeek)),
    [operatingHours],
  );

  const days = useMemo(() => {
    const result: string[] = [];
    let current = bookableFrom;
    while (current <= bookableUntil) {
      result.push(current);
      current = addDays(current, 1);
    }
    return result;
  }, [bookableFrom, bookableUntil]);

  return (
    <div className="flex gap-2 overflow-x-auto pb-2 [scrollbar-width:none] [&::-webkit-scrollbar]:display-none">
      {days.map((date) => {
        const isOpen = openDays.has(dayOfWeek(date));
        const isSelected = date === selectedDate;
        const { weekday, day, month } = formatDayLabel(date);

        return (
          <button
            key={date}
            type="button"
            disabled={!isOpen}
            onClick={() => onSelect(date)}
            data-selected={isSelected || undefined}
            className="flex shrink-0 flex-col items-center gap-1 px-2 disabled:cursor-not-allowed disabled:opacity-40"
          >
            <span className="text-xs font-medium text-slate-500">
              {weekday}
            </span>
            <span
              className={`flex h-11 w-11 items-center justify-center rounded-full text-sm font-semibold ring-1 ${
                isSelected
                  ? "bg-brand text-white ring-brand"
                  : "bg-white text-slate-700 ring-slate-200"
              }`}
            >
              {day}
            </span>
            <span className="text-xs text-slate-500">{month}</span>
          </button>
        );
      })}
    </div>
  );
}
