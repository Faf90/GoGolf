export interface Bay {
  id: number;
  name: string;
  description: string | null;
}

export interface PlayerRate {
  playerCount: number;
  price: number;
}

export interface OperatingHours {
  dayOfWeek: number;
  opensAt: string;
  closesAt: string;
}

export interface ClubSummary {
  id: number;
  name: string;
  address: string;
  fromPrice: number;
  rating: number;
  bayCount: number;
}

export interface ClubDetail {
  id: number;
  name: string;
  address: string;
  fromPrice: number;
  rating: number;
  bookableFrom: string;
  bookableUntil: string;
  operatingHours: OperatingHours[];
  bays: Bay[];
  playerRates: PlayerRate[];
}