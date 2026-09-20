import { useState, useEffect } from "react";
import ClubCard from "./components/ClubCard";
import ApiStatus from "./components/ApiStatus";

export default function App() {
  const [clubs, setClubs] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    fetch("/api/clubs")
      .then((res) => {
        if (!res.ok) throw new Error(`HTTP ${res.status}`);
        return res.json();
      })
      .then(setClubs)
      .catch((err) => setError(err.message))
      .finally(() => setLoading(false));
  }, []);

  return (
    <div className="min-h-screen bg-slate-50">
      <header className="border-b border-slate-200 bg-white">
        <div className="mx-auto max-w-6xl px-4 py-4">
          <h1 className="text-xl font-bold text-emerald-700">TeeTime</h1>
          <ApiStatus />
        </div>
      </header>

      <main className="mx-auto max-w-6xl px-4 py-8">
        <h2 className="text-2xl font-bold text-slate-900">Golf clubs near you</h2>

        {loading && <p className="mt-1 text-slate-500">Loading clubs…</p>}

        {error && (
          <p className="mt-1 text-red-600">Couldn't load clubs: {error}</p>
        )}

        {!loading && !error && (
          <>
            <p className="mt-1 text-slate-500">{clubs.length} clubs available</p>

            <div className="mt-6 grid grid-cols-1 gap-5 sm:grid-cols-2 lg:grid-cols-3">
              {clubs.map((club) => (
                <ClubCard key={club.id} club={club} />
              ))}
            </div>
          </>
        )}
      </main>
    </div>
  );
}