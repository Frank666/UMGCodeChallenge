import { usageType } from "./usageType";

export interface MusicContract {
  artist: string;
  title: string;
  usages: usageType[];
  startDate: string;
  endDate?: string | null;
}