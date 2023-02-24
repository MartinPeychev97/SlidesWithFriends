import React, { useContext } from "react";
import SlideContext from "../../context/SlideContext";
import styles from "./preview.module.css";

const Preview = ({ index, slide }) => {
    const { setActiveSlide, activeSlide, setIsAddNewSlideOpen, presentation } = useContext(SlideContext);
    const selectSlide = () => {
        setActiveSlide(slide)
        setIsAddNewSlideOpen(false)
    }

    const rating = Array(slide.rating)
    for (var i = 1; i <= slide.rating; i++) {
        rating.push(i)
    }

    return (
        <div onClick={selectSlide} className={`${styles.slidePreview} ${activeSlide.id === slide.id ? styles.active : undefined}`}>
            <p className={styles.slideNumber}>{index}</p>
            <div className={styles.slideFrame}>
                <img
                    className={styles.slideBackground}
                    src={presentation.image}
                />
                <div className={styles.content}>
                    <h6>{slide.title}</h6>
                    <img src={slide.image} alt="" />
                    <div className={styles.content}>
                        {rating.map((number, index) => <svg key={index} fill="currentColor" width="12"
                            xmlns="http://www.w3.org/2000/svg" viewBox="0 0 576 512"><path d="M316.9 18C311.6 7 300.4 0 288.1 0s-23.4 7-28.8 18L195 150.3 51.4 171.5c-12 1.8-22 10.2-25.7 21.7s-.7 24.2 7.9 32.7L137.8 329 113.2 474.7c-2 12 3 24.2 12.9 31.3s23 8 33.8 2.3l128.3-68.5 128.3 68.5c10.8 5.7 23.9 4.9 33.8-2.3s14.9-19.3 12.9-31.3L438.5 329 542.7 225.9c8.6-8.5 11.7-21.2 7.9-32.7s-13.7-19.9-25.7-21.7L381.2 150.3 316.9 18z" /></svg>)}
                    </div>
                </div>
                <div style={{ background: slide.background }} className={styles.slidePreviewOverlay}></div>
            </div>
        </div>
    );
};

export default Preview;
