import ClubCard from "../features/clubs/components/ClubCard";
import { useClubs } from "../features/clubs/hooks/useClubs";

export default function ClubsPage() {
  const { clubs, status, error } = useClubs();

  return (
    <>
      <h2 className="text-2xl font-bold text-slate-900">Golf clubs near you</h2>

      {status === "loading" && (
        <p className="mt-1 text-slate-500">Loading clubs…</p>
      )}

      {status === "error" && (
        <p className="mt-1 text-red-600">
          Couldn't load clubs: {error?.message ?? "unknown error"}
        </p>
      )}

      {status === "ready" && (
        <>
          <p className="mt-1 text-slate-500">{clubs.length} clubs available</p>

          <div className="mt-6 grid grid-cols-1 gap-5 sm:grid-cols-2 lg:grid-cols-3">
            {clubs.map((club) => (
              <ClubCard key={club.id} club={club} />
            ))}
          </div>
        </>
      )}
    </>
  );
}
