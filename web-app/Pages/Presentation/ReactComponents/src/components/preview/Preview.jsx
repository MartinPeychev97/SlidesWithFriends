import React, { useContext } from "react";
import SlideContext from "../../context/SlideContext";
import styles from "./preview.module.css";

const Preview = ({index, slide}) => {
  const {setActiveSlide, activeSlide} = useContext(SlideContext);

  const selectSlide = () => {
    setActiveSlide(slide)
  }

  return (
    <div onClick={selectSlide} className={`${styles.slidePreview} ${activeSlide.id === slide.id ? styles.active : undefined}`}>
      <p className={styles.slideNumber}>{index}</p>
      <div className={styles.slideFrame}>
        <img
          className={styles.slideBackground}
          src="https://slideswith.com/cdn-cgi/image/w=1900,h=1400,fit=scale-down,metadata=none,onerror=false/https://slideswith.com//backgrounds/background-20.jpg"
          alt=""
        />
        <div className={styles.content}>
          <h6>{slide.title}</h6>
        </div>
      </div>
    </div>
  );
};

export default Preview;
