import React, { useContext } from "react";
import styles from "./app.module.css";

import Header from "./components/header/Header";
import SidePane from "./components/side-pane/SidePane";
import Main from "./components/main/Main";
import SlideContext, { SlideProvider } from "./context/SlideContext";
import AddSlide from "./components/add-slide/AddSlide";

const App = () => {
  const { slides, isAddNewSlideOpen } = useContext(SlideContext);

  return (
    <div className={styles.editor}>
        <Header />
          <SidePane />
          { isAddNewSlideOpen && <AddSlide /> }
          { slides.length > 0 && <Main /> }
    </div>
  );
};

export default App;
