export function parseDateOnly(value: string): Date {
  const [year, month, day] = value.split("-").map(Number);
  return new Date(year, month - 1, day); // local midnight, no UTC shift
}

export function toDateOnly(date: Date): string {
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, "0");
  const day = String(date.getDate()).padStart(2, "0");
  return `${year}-${month}-${day}`;
}

export function addDays(value: string, days: number): string {
  const date = parseDateOnly(value);
  date.setDate(date.getDate() + days);
  return toDateOnly(date);
}

export function dayOfWeek(value: string): number {
  return parseDateOnly(value).getDay(); // 0 = Sunday, matches C#
}

export function formatDayLabel(value: string) {
  const date = parseDateOnly(value);
  return {
    weekday: date
      .toLocaleDateString("en-ZA", { weekday: "short" })
      .toUpperCase(),
    day: String(date.getDate()).padStart(2, "0"),
    month: date.toLocaleDateString("en-ZA", { month: "short" }),
  };
}
