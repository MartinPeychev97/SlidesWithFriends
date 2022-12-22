import React, { useContext } from "react";
import SlideContext from "../../context/SlideContext";
import useFetch from "../../hooks/useFetch";
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
    setIsAddNewSlideOpen
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
    const activeSlideIndex = slides.indexOf(activeSlide);
    if(activeSlideIndex == 0 && slides.length > 0){
      setActiveSlide(slides[activeSlideIndex + 1])
    }
    else{
      setActiveSlide(slides[activeSlideIndex - 1])
    }
    setSlides(slides.filter(s => s.id != activeSlide.id))
  };

  const openAddNewSlide = () => {
    setIsAddNewSlideOpen(true)
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
        <svg
          xmlns="http://www.w3.org/2000/svg"
          viewBox="0 0 512 512"
          width="20"
          fill="currentColor"
        >
          <path d="M512 256c0 .9 0 1.8 0 2.7c-.4 36.5-33.6 61.3-70.1 61.3H344c-26.5 0-48 21.5-48 48c0 3.4 .4 6.7 1 9.9c2.1 10.2 6.5 20 10.8 29.9c6.1 13.8 12.1 27.5 12.1 42c0 31.8-21.6 60.7-53.4 62c-3.5 .1-7 .2-10.6 .2C114.6 512 0 397.4 0 256S114.6 0 256 0S512 114.6 512 256zM128 288c0-17.7-14.3-32-32-32s-32 14.3-32 32s14.3 32 32 32s32-14.3 32-32zm0-96c17.7 0 32-14.3 32-32s-14.3-32-32-32s-32 14.3-32 32s14.3 32 32 32zM288 96c0-17.7-14.3-32-32-32s-32 14.3-32 32s14.3 32 32 32s32-14.3 32-32zm96 96c17.7 0 32-14.3 32-32s-14.3-32-32-32s-32 14.3-32 32s14.3 32 32 32z" />
        </svg>
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
      </div>
    </header>
  );
};

export default Header;
