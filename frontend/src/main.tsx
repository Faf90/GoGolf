import React from "react";
import ReactDom from "react-dom/client";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import ClubsPage from "./routes/ClubsPage";
import ClubDetailPage from "./routes/ClubDetailPage";
import Layout from "./components/Layout";
import "./index.css";

const router = createBrowserRouter([
  {
    path: "/",
    element: <Layout />,
    children: [
      { index: true, element: <ClubsPage /> },
      { path: "clubs/:clubId", element: <ClubDetailPage /> },
      { path: "*", element: <NotFound /> },
    ],
  },
]);

function NotFound() {
  return <p>Page not found.</p>;
}

ReactDom.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>,
);
