import { useState, useEffect } from "react";
import { api, ApiError } from "../../../lib/apiClient";
import type { AvailableSlots } from "../types";

type Status = "loading" | "ready" | "error";

export function useAvailableSlots(
  clubId: string | undefined,
  date: string | null,
) {
  const [data, setData] = useState<AvailableSlots | null>(null);
  const [status, setStatus] = useState<Status>("loading");
  const [error, setError] = useState<ApiError | null>(null);

  useEffect(() => {
    if (!clubId || !date) return;

    let cancelled = false;
    setStatus("loading");

    api
      .get<AvailableSlots>(`/api/clubs/${clubId}/available-slots?date=${date}`)
      .then((result) => {
        if (cancelled) return;
        setData(result);
        setStatus("ready");
      })
      .catch((err) => {
        if (cancelled) return;
        setError(err instanceof ApiError ? err : null);
        setStatus("error");
      });

    return () => {
      cancelled = true;
    };
  }, [clubId, date]);

  return { data, status, error };
}
