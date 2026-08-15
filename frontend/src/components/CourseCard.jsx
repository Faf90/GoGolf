export default function CourseCard({ course }) {
  return (
    <div className="overflow-hidden rounded-2xl bg-white shadow-sm ring-1 ring-slate-200 transition hover:shadow-md">
      <div className={`h-40 bg-gradient-to-br ${course.image} flex items-end p-4`}>
        <span className="rounded-full bg-white/90 px-2.5 py-1 text-xs font-semibold text-slate-700">
          ★ {course.rating}
        </span>
      </div>

      <div className="p-4">
        <h2 className="font-semibold text-slate-900">{course.name}</h2>
        <p className="mt-0.5 text-sm text-slate-500">{course.location}</p>

        <div className="mt-3 flex items-center gap-3 text-xs text-slate-600">
          <span>{course.holes} holes</span>
          <span className="text-slate-300">•</span>
          <span>Par {course.par}</span>
        </div>

        <div className="mt-4 flex items-center justify-between">
          <div>
            <span className="text-lg font-bold text-slate-900">R{course.price}</span>
            <span className="text-sm text-slate-500"> / round</span>
          </div>
          <button className="rounded-lg bg-emerald-600 px-4 py-2 text-sm font-medium text-white transition hover:bg-emerald-700">
            Book
          </button>
        </div>
      </div>
    </div>
  );
}