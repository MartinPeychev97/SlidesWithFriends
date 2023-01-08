import React, { useContext } from "react";
import SlideContext from "../../context/SlideContext";
import styles from "./preview.module.css";

const Preview = ({ index, slide }) => {
    const { setActiveSlide, activeSlide, setIsAddNewSlideOpen, slides } = useContext(SlideContext);
    const selectSlide = () => {
        setActiveSlide(slide)
        setIsAddNewSlideOpen(false)
    }

    return (
        <div onClick={selectSlide} className={`${styles.slidePreview} ${activeSlide.id === slide.id ? styles.active : undefined}`}>
            <p className={styles.slideNumber}>{index}</p>
            <div className={styles.slideFrame}>
                <img
                    className={styles.slideBackground}
                    src={slide.image}
                />
                <div className={styles.content}>
                    <h6>{slide.title}</h6>
                </div>
                <div style={{ background: slide.background }} className={styles.slidePreviewOverlay}></div>
            </div>
        </div>
    );
};

export default Preview;
