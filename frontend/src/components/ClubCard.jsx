const gradients = [
  "from-emerald-500 to-teal-600",
  "from-sky-500 to-blue-600",
  "from-lime-500 to-green-600",
  "from-cyan-500 to-teal-600",
  "from-teal-500 to-emerald-700",
];

const gradientFor = (id) => gradients[id % gradients.length];

export default function ClubCard({ club }) {
  return (
    <div className="overflow-hidden rounded-2xl bg-white shadow-sm ring-1 ring-slate-200 transition hover:shadow-md">
      <div
        className={`h-40 bg-gradient-to-br ${gradientFor(club.id)} flex items-end p-4`}
      >
        <span className="rounded-full bg-white/90 px-2.5 py-1 text-xs font-semibold text-slate-700">
          ★ {club.rating}
        </span>
      </div>

      <div className="p-4">
        <h2 className="font-semibold text-slate-900">{club.name}</h2>
        <p className="mt-0.5 text-sm text-slate-500">{club.address}</p>

        <div className="mt-4 flex items-center justify-between">
          <div>
            <span className="text-lg font-bold text-slate-900">
              R{club.fromPrice}
            </span>
            <span className="text-sm text-slate-500"> / hour</span>
          </div>
          <button className="rounded-lg bg-emerald-600 px-4 py-2 text-sm font-medium text-white transition hover:bg-emerald-700">
            Book
          </button>
        </div>
      </div>
    </div>
  );
}