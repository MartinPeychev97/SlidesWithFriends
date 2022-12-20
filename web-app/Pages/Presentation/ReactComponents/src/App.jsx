import React from "react";
import styles from "./app.module.css";

import Header from "./components/header/Header";
import SidePane from "./components/side-pane/SidePane";
import Main from "./components/main/Main";
import { SlideProvider } from "./context/SlideContext";

const App = () => {
  const presentationId = window.location.href.split("/").pop();

  return (
    <div className={styles.editor}>
      <SlideProvider>
        <Header />
        <div className={styles.slidesContainer}>
          <SidePane />
          <Main />
        </div>
      </SlideProvider>
    </div>
  );
};

export default App;
