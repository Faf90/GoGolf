export interface AvailableBay {
  id: number;
  name: string;
  description: string | null;
}

export interface PlayerRate {
  playerCount: number;
  price: number;
}

export interface Slot {
  startsAt: string; // "2026-10-01T06:00:00+00:00"
  endsAt: string;
  localTime: string; // "08:00:00"
  bays: AvailableBay[];
}

export interface AvailableSlots {
  clubId: number;
  date: string;
  isOpen: boolean;
  slotDurationMinutes: number;
  playerRates: PlayerRate[];
  slots: Slot[];
}
