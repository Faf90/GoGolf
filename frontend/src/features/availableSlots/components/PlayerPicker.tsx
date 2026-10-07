import type { PlayerRate } from "../types";

interface Props {
  rates: PlayerRate[];
  selectedCount: number | null;
  onSelect: (count: number) => void;
}

export default function PlayerPicker({
  rates,
  selectedCount,
  onSelect,
}: Props) {
  return (
    <div className="flex flex-wrap gap-2">
      {rates.map((rate) => {
        const isSelected = rate.playerCount === selectedCount;

        return (
          <button
            key={rate.playerCount}
            type="button"
            onClick={() => onSelect(rate.playerCount)}
            className={`flex min-w-20 flex-col items-center rounded-lg px-4 py-2.5 ring-1 transition ${
              isSelected
                ? "bg-brand text-white ring-brand"
                : "bg-white text-slate-700 ring-slate-200 hover:ring-brand-hover"
            }`}
          >
            <span className="text-sm font-semibold">
              {rate.playerCount} {rate.playerCount === 1 ? "player" : "players"}
            </span>
            <span
              className={`text-xs ${isSelected ? "text-slate-300" : "text-slate-500"}`}
            >
              R{rate.price}
            </span>
          </button>
        );
      })}
    </div>
  );
}
