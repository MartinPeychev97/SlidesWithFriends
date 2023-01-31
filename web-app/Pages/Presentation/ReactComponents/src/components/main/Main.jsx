import React, { useContext, useEffect } from "react";
import SlideContext from "../../context/SlideContext";
import useFetch from "../../hooks/useFetch";
import styles from "./main.module.css";

const Main = () => {
    const {
        activeSlide,
        setActiveSlide,
        slides,
        setSlides,
        setIsAddNewSlideOpen,
    } = useContext(SlideContext);
    const { post } = useFetch();

    const rating = Array(activeSlide.rating)
    for (var i = 1; i <= activeSlide.rating; i++) {
        rating.push(i)
    }

    const editTitle = async (e) => {
        const title = e.target.innerText;

        if (title !== activeSlide.title) {
            await post(`slide/editTitle`, {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({
                    id: activeSlide.id,
                    title: title,
                }),
            });

            setActiveSlide({ ...activeSlide, title: title });

            setSlides(
                slides.map((slide) => {
                    if (slide.id === activeSlide.id) {
                        return {
                            ...slide,
                            title: title,
                        };
                    }
                    return slide;
                })
            );
        }
    };

    const editText = async (e) => {
        const text = e.target.innerText;

        if (text !== activeSlide.text) {
            await post(`slide/editText`, {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({
                    id: activeSlide.id,
                    text: text,
                }),
            });

            setActiveSlide({ ...activeSlide, text: text });

            setSlides(
                slides.map((slide) => {
                    if (slide.id === activeSlide.id) {
                        return {
                            ...slide,
                            text: text,
                        };
                    }
                    return slide;
                })
            );
        }
    };

    const closeAddNewSlide = () => {
        setIsAddNewSlideOpen(false);
    };

    return (
        <div onClick={closeAddNewSlide} className={styles.slideEdit}>
            <div className={styles.slideFrame}>
                <img
                    className={styles.slideBackground}
                    src="https://slideswith.com/cdn-cgi/image/w=1900,h=1400,fit=scale-down,metadata=none,onerror=false/https://slideswith.com//backgrounds/background-20.jpg"
                />
                {activeSlide.type === "Title" && (
                    <div className={styles.content}>
                        <h1 onBlur={editTitle} className={styles.title}>
                            {activeSlide.title}
                        </h1>
                        <p onBlur={editText} className={styles.text}>
                            {activeSlide.text}
                        </p>
                    </div>
                )}
                {activeSlide.type === "Image" && (
                    <div className={`${styles.content} ${styles.imageSlide}`}>
                        <img className={styles.image} src={activeSlide.image} alt="" />
                        <p onBlur={editText} className={styles.text}>
                            {activeSlide.text}
                        </p>
                    </div>
                )}
                {activeSlide.type === "Rating" && (
                    <div className={styles.content}>
                        {rating.map((i) => <svg key={i} width="38" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 576 512"><path d="M316.9 18C311.6 7 300.4 0 288.1 0s-23.4 7-28.8 18L195 150.3 51.4 171.5c-12 1.8-22 10.2-25.7 21.7s-.7 24.2 7.9 32.7L137.8 329 113.2 474.7c-2 12 3 24.2 12.9 31.3s23 8 33.8 2.3l128.3-68.5 128.3 68.5c10.8 5.7 23.9 4.9 33.8-2.3s14.9-19.3 12.9-31.3L438.5 329 542.7 225.9c8.6-8.5 11.7-21.2 7.9-32.7s-13.7-19.9-25.7-21.7L381.2 150.3 316.9 18z" /></svg>)}
                    </div>
                )}
                <div
                    style={{ background: activeSlide.background }}
                    className={styles.slideOverlay}
                ></div>
            </div>
        </div>
    );
};

export default Main;
