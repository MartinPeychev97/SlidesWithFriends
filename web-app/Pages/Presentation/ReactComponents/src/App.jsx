import React, { useContext } from "react";
import styles from "./app.module.css";

import Header from "./components/header/Header";
import SidePane from "./components/side-pane/SidePane";
import Main from "./components/main/Main";
import SlideContext, { SlideProvider } from "./context/SlideContext";

const App = () => {
  const { slides } = useContext(SlideContext);

  return (
    <div className={styles.editor}>
        <Header />
        <div className={styles.slidesContainer}>
          <SidePane />
          { slides.length > 0 && <Main /> }
        </div>
    </div>
  );
};

export default App;
