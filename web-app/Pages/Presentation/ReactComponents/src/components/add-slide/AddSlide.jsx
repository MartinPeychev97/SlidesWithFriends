import React, { useContext, useRef } from "react";
import SlideContext from "../../context/SlideContext";
import useFetch from "../../hooks/useFetch";
import styles from "./add-slide.module.css";

const AddSlide = () => {
    const {
        setIsAddNewSlideOpen,
        presentationId,
        setSlides,
        slides,
        setActiveSlide,
    } = useContext(SlideContext);
    const { post } = useFetch();
    const fileInput = useRef(null);
    const fileInputContainer = useRef(null);

    const addTitleSlide = async () => {
        const slide = await post(`slide/addTitleSlide?presentationId=${presentationId}`, {
            method: "POST",
        });

        setSlides([...slides, slide]);
        setActiveSlide(slide);
        setIsAddNewSlideOpen(false);
    };

    const addImageSlide = async (event) => {
        const image = event.target.files[0];

        const formData = new FormData();
        formData.append("image", image);
        formData.append("presentationId", +presentationId)

        const slide = await post(`slide/addImageSlide`, {
            method: "POST",
            body: formData
        });

        setSlides([...slides, slide]);
        setActiveSlide(slide);
        setIsAddNewSlideOpen(false);
    };

    const addRatingSlide = async () => {
        const slide = await post('slide/addRatingSlide', {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ presentationId : +presentationId, rating: 0 })
        })
        setSlides([...slides, slide]);
        setActiveSlide(slide);
        setIsAddNewSlideOpen(false);
    }

    const addWordCloudSlide = async () => {
        const slide = await post(`slide/addWordCloudSlide?presentationId=${presentationId}`, {
            method: "POST",
        });

        setSlides([...slides, slide]);
        setActiveSlide(slide);
        setIsAddNewSlideOpen(false);
    };

    const closeAddNewSlide = () => {
        setIsAddNewSlideOpen(false);
    };

    const handleClick = () => {
        fileInput.current.click();
    };

    return (
        <div className={styles.addNewSlide}>
            <div className={styles.menuContainer}>
                <h2 className={styles.menuTitle}>Add New Slide</h2>
                <svg
                    onClick={closeAddNewSlide}
                    className={styles.closeMenu}
                    xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 320 512"
                    fill="currentColor"
                >
                    <path d="M310.6 150.6c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 0L160 210.7 54.6 105.4c-12.5-12.5-32.8-12.5-45.3 0s-12.5 32.8 0 45.3L114.7 256 9.4 361.4c-12.5 12.5-12.5 32.8 0 45.3s32.8 12.5 45.3 0L160 301.3 265.4 406.6c12.5 12.5 32.8 12.5 45.3 0s12.5-32.8 0-45.3L205.3 256 310.6 150.6z" />
                </svg>
                <div className={styles.slideTypes}>
                    <div className={styles.slideType} onClick={addTitleSlide}>
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            viewBox="0 0 448 512"
                            width="38"
                            fill="currentColor"
                        >
                            <path d="M352 64c0-17.7-14.3-32-32-32H128c-17.7 0-32 14.3-32 32s14.3 32 32 32H320c17.7 0 32-14.3 32-32zm96 128c0-17.7-14.3-32-32-32H32c-17.7 0-32 14.3-32 32s14.3 32 32 32H416c17.7 0 32-14.3 32-32zM0 448c0 17.7 14.3 32 32 32H416c17.7 0 32-14.3 32-32s-14.3-32-32-32H32c-17.7 0-32 14.3-32 32zM352 320c0-17.7-14.3-32-32-32H128c-17.7 0-32 14.3-32 32s14.3 32 32 32H320c17.7 0 32-14.3 32-32z" />
                        </svg>
                        <p>Title Slide</p>
                    </div>
                    <div
                        onClick={handleClick}
                        ref={fileInputContainer}
                        className={styles.slideType}
                    >
                        <input
                            type="file"
                            ref={fileInput}
                            onChange={addImageSlide}
                            style={{ display: "none" }}
                            accept=".jpg, .png, .gif"
                        />
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            viewBox="0 0 576 512"
                            width="38"
                            fill="currentColor"
                        >
                            <path d="M160 32c-35.3 0-64 28.7-64 64V320c0 35.3 28.7 64 64 64H512c35.3 0 64-28.7 64-64V96c0-35.3-28.7-64-64-64H160zM396 138.7l96 144c4.9 7.4 5.4 16.8 1.2 24.6S480.9 320 472 320H328 280 200c-9.2 0-17.6-5.3-21.6-13.6s-2.9-18.2 2.9-25.4l64-80c4.6-5.7 11.4-9 18.7-9s14.2 3.3 18.7 9l17.3 21.6 56-84C360.5 132 368 128 376 128s15.5 4 20 10.7zM256 128c0 17.7-14.3 32-32 32s-32-14.3-32-32s14.3-32 32-32s32 14.3 32 32zM48 120c0-13.3-10.7-24-24-24S0 106.7 0 120V344c0 75.1 60.9 136 136 136H456c13.3 0 24-10.7 24-24s-10.7-24-24-24H136c-48.6 0-88-39.4-88-88V120z" />
                        </svg>
                        <p>Image Slide</p>
                    </div>
                    <div className={styles.slideType} onClick={addRatingSlide}>
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            viewBox="0 0 448 512"
                            width="38"
                            fill="currentColor"
                        >
                            <path d="M352 64c0-17.7-14.3-32-32-32H128c-17.7 0-32 14.3-32 32s14.3 32 32 32H320c17.7 0 32-14.3 32-32zm96 128c0-17.7-14.3-32-32-32H32c-17.7 0-32 14.3-32 32s14.3 32 32 32H416c17.7 0 32-14.3 32-32zM0 448c0 17.7 14.3 32 32 32H416c17.7 0 32-14.3 32-32s-14.3-32-32-32H32c-17.7 0-32 14.3-32 32zM352 320c0-17.7-14.3-32-32-32H128c-17.7 0-32 14.3-32 32s14.3 32 32 32H320c17.7 0 32-14.3 32-32z" />
                        </svg>
                        <p>RatingSlide</p>
                    </div>
                    <div className={styles.slideType} onClick={addWordCloudSlide}>
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            viewBox="0 0 448 512"
                            width="38"
                            fill="currentColor"
                        >
                            <path d="M352 64c0-17.7-14.3-32-32-32H128c-17.7 0-32 14.3-32 32s14.3 32 32 32H320c17.7 0 32-14.3 32-32zm96 128c0-17.7-14.3-32-32-32H32c-17.7 0-32 14.3-32 32s14.3 32 32 32H416c17.7 0 32-14.3 32-32zM0 448c0 17.7 14.3 32 32 32H416c17.7 0 32-14.3 32-32s-14.3-32-32-32H32c-17.7 0-32 14.3-32 32zM352 320c0-17.7-14.3-32-32-32H128c-17.7 0-32 14.3-32 32s14.3 32 32 32H320c17.7 0 32-14.3 32-32z" />
                        </svg>
                        <p>WordCloud Slide</p>
                    </div>
                </div>

            </div>
        </div>
    );
};

export default AddSlide;
