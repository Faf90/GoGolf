import { useEffect, useState } from "react";
import { api, ApiError } from "../../../lib/apiClient";
import type { ClubSummary } from "../types";

type Status = "loading" | "ready" | "error";

export function useClubs() {
  const [clubs, setClubs] = useState<ClubSummary[]>([]);
  const [status, setStatus] = useState<Status>("loading");
  const [error, setError] = useState<ApiError | null>(null);

  useEffect(() => {
    let cancelled = false;

    api
      .get<ClubSummary[]>("/api/clubs")
      .then((data) => {
        if (cancelled) return;
        setClubs(data);
        setStatus("ready");
      })
      .catch((err: unknown) => {
        if (cancelled) return;
        setError(err instanceof ApiError ? err : null);
        setStatus("error");
      });

    return () => {
      cancelled = true;
    };
  }, []);

  return { clubs, status, error };
}