import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App";
import { SlideProvider } from "./context/SlideContext";

ReactDOM.createRoot(document.getElementById("edit-presentation")).render(
  <React.StrictMode>
    <SlideProvider>
      <App />
    </SlideProvider>
  </React.StrictMode>
);
