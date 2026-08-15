import { useState, useEffect } from "react";

const API_URL = import.meta.env.VITE_API_URL ?? "http://localhost:5012";

export default function ApiStatus() {
  const [status, setStatus] = useState("checking");

  useEffect(() => {
    fetch(`${API_URL}/api/health`)
      .then((res) => {
        if (!res.ok) throw new Error(`HTTP ${res.status}`);
        return res.json();
      })
      .then((data) => setStatus(data.status))
      .catch(() => setStatus("unreachable"));
  }, []);

  const styles = {
    checking: "bg-slate-100 text-slate-600",
    healthy: "bg-emerald-100 text-emerald-700",
    unreachable: "bg-red-100 text-red-700",
  };

  return (
    <span className={`rounded-full px-2.5 py-1 text-xs font-medium ${styles[status]}`}>
      API: {status}
    </span>
  );
}