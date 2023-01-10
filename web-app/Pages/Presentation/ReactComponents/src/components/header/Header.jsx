import React, { useContext } from "react";
import SlideContext from "../../context/SlideContext";
import useFetch from "../../hooks/useFetch";
import ColorPicker from "../color-picker/ColorPicker";
import styles from "./header.module.css";

const Header = () => {
    const {
        presentationName,
        setPresentationName,
        presentationId,
        setSlides,
        slides,
        setActiveSlide,
        activeSlide,
        setIsAddNewSlideOpen,
    } = useContext(SlideContext);

    const { post } = useFetch();

    const handleChange = (e) => {
        setPresentationName(e.target.value);
    };

    const editName = async (e) => {
        const name = e.target.value;

        await post(`presentation/editName`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                id: +presentationId,
                name: name,
            }),
        });
    };

    const removeSlide = async () => {
        await post(`slide/remove?id=${activeSlide.id}`, {
            method: "DELETE",
        });

        const activeSlideIndex = slides.findIndex(s => s.id === activeSlide.id);

        if (activeSlideIndex == 0 && slides.length > 0) {
            setActiveSlide(slides[activeSlideIndex + 1]);
        } else {
            setActiveSlide(slides[activeSlideIndex - 1]);
        }
        setSlides(slides.filter((s) => s.id != activeSlide.id));
    };

    const openAddNewSlide = () => {
        setIsAddNewSlideOpen(true);
    };

    const startEventHandler = () => {
        window.open(`/event/presentation/${presentationId}`)
    }

    return (
        <header className={styles.header}>
            <div className={styles.presentationName}>
                <input
                    onBlur={editName}
                    onChange={handleChange}
                    type="text"
                    value={presentationName}
                />
            </div>
            <div className={styles.actions}>
                <svg
                    onClick={openAddNewSlide}
                    xmlns="http://www.w3.org/2000/svg"
                    viewBox="0 0 448 512"
                    width="20"
                    fill="currentColor"
                >
                    <path d="M256 80c0-17.7-14.3-32-32-32s-32 14.3-32 32V224H48c-17.7 0-32 14.3-32 32s14.3 32 32 32H192V432c0 17.7 14.3 32 32 32s32-14.3 32-32V288H400c17.7 0 32-14.3 32-32s-14.3-32-32-32H256V80z" />
                </svg>
                <ColorPicker />
                {slides.length > 0 && (
                    <svg
                        onClick={removeSlide}
                        xmlns="http://www.w3.org/2000/svg"
                        width="20"
                        fill="currentColor"
                        className="bi bi-trash-fill"
                        viewBox="0 0 16 16"
                    >
                        <path d="M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1H2.5zm3 4a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 .5-.5zM8 5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7A.5.5 0 0 1 8 5zm3 .5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 1 0z" />
                    </svg>
                )}
            </div>
            <div className={styles.actionsRight}>
                <button className={styles.startEvent} onClick={startEventHandler} >Start an Event</button>
            </div>
        </header>
    );
};

export default Header;
