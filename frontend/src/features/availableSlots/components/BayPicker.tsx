// src/features/availableSlots/components/BayPicker.tsx
import type { AvailableBay } from "../types";

interface Props {
  bays: AvailableBay[];
  selectedBayId: number | null;
  onSelect: (bayId: number) => void;
}

export default function BayPicker({ bays, selectedBayId, onSelect }: Props) {
  if (bays.length === 0) {
    return <p className="text-slate-500">No bays available at this time.</p>;
  }

  return (
    <div className="space-y-2">
      {bays.map((bay) => {
        const isSelected = bay.id === selectedBayId;

        return (
          <button
            key={bay.id}
            type="button"
            onClick={() => onSelect(bay.id)}
            className={`flex w-full items-center justify-between rounded-xl px-4 py-3 text-left ring-1 transition ${
              isSelected
                ? "bg-brand ring-brand"
                : "bg-white ring-slate-200 hover:ring-brand-hover"
            }`}
          >
            <div>
              <p
                className={`font-semibold ${isSelected ? "text-white" : "text-slate-900"}`}
              >
                {bay.name}
              </p>
              {bay.description && (
                <p
                  className={`text-sm ${isSelected ? "text-slate-300" : "text-slate-500"}`}
                >
                  {bay.description}
                </p>
              )}
            </div>
          </button>
        );
      })}
    </div>
  );
}
