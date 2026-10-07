import { useEffect, useState } from "react";
import { api, ApiError } from "../../../lib/apiClient";
import type { ClubDetail } from "../types";

type Status = "loading" | "ready" | "error";
const apiPrefix = '/api/clubs/';

export function useClub(id: string | undefined) {
    const [club, setClub] = useState<ClubDetail | null>(null);
    const [status, setStatus] = useState<Status>("loading");
    const [error, setError] = useState<ApiError | null>(null);

    useEffect(() => {
        if (!id) {
            setStatus("error");
            return;
        };

        let cancelled = false;
        setStatus("loading");

        api
            .get<ClubDetail>(`${apiPrefix}${id}`)
            .then((data) => {
                if (cancelled) return; 
                    setClub(data);
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

    }, [id]);

    return { club, status, error };
}
