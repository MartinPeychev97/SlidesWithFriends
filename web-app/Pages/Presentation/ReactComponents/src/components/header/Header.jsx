import React, { useContext, useRef } from "react";
import SlideContext from "../../context/SlideContext";
import useFetch from "../../hooks/useFetch";
import ColorPicker from "../color-picker/ColorPicker";
import styles from "./header.module.css";

const Header = () => {
  const {
    presentation,
    setPresentation,
    presentationId,
    setSlides,
    slides,
    setActiveSlide,
    activeSlide,
    setIsAddNewSlideOpen,
  } = useContext(SlideContext);

  const { post } = useFetch();
  const fileInput = useRef(null);
  const fileInputContainer = useRef(null);

  const handleChange = (e) => {
    setPresentation({ ...presentation, name: e.target.value });
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

    const activeSlideIndex = slides.findIndex((s) => s.id === activeSlide.id);

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

        $.ajax({
            type: "POST",
            url: "/rating/ClearVotes",
            data: { presentationId: presentationId }
        });


    window.open(`/event/presentation/${presentationId}?isPresenter=true`);
  };

  const changePresentationImage = async (event) => {
    const image = event.target.files[0];

    const formData = new FormData();
    formData.append("image", image);
    formData.append("id", +presentationId);

    const imageUrl = await post(`presentation/editImage`, {
        method: "PUT",
        body: formData
    });

    setPresentation({...presentation, image: imageUrl.value});
  };

  const handleClick = () => {
    fileInput.current.click();
};

  return (
    <header className={styles.header}>
      <div className={styles.presentationName}>
        <input
          onBlur={editName}
          onChange={handleChange}
          type="text"
          value={presentation.name || ""}
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
        <div ref={fileInputContainer} onClick={handleClick}>
          <input
            type="file"
            ref={fileInput}
            onChange={changePresentationImage}
            style={{ display: "none" }}
            accept=".jpg, .png, .gif"
          />
          <svg
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 512 512"
            width="20"
            fill="currentColor"
          >
            <path d="M0 96C0 60.7 28.7 32 64 32H448c35.3 0 64 28.7 64 64V416c0 35.3-28.7 64-64 64H64c-35.3 0-64-28.7-64-64V96zM323.8 202.5c-4.5-6.6-11.9-10.5-19.8-10.5s-15.4 3.9-19.8 10.5l-87 127.6L170.7 297c-4.6-5.7-11.5-9-18.7-9s-14.2 3.3-18.7 9l-64 80c-5.8 7.2-6.9 17.1-2.9 25.4s12.4 13.6 21.6 13.6h96 32H424c8.9 0 17.1-4.9 21.2-12.8s3.6-17.4-1.4-24.7l-120-176zM112 192a48 48 0 1 0 0-96 48 48 0 1 0 0 96z" />
          </svg>
        </div>
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
        <button className={styles.startEvent} onClick={startEventHandler}>
          Start an Event
        </button>
      </div>
    </header>
  );
};

export default Header;
