import React, { useContext } from "react";
import SlideContext from "../../context/SlideContext";
import useFetch from "../../hooks/useFetch";
import styles from "./main.module.css";

const Main = () => {
  const { activeSlide, slides, setSlides, setActiveSlide } = useContext(SlideContext);
  const { post } = useFetch();

  const removeSlide = async () => {
    await post(`slide/remove?id=${activeSlide.id}`, {
      method: "DELETE",
    });
    const activeSlideIndex = slides.indexOf(activeSlide);
    setActiveSlide(slides[activeSlideIndex - 1])
    setSlides(slides.filter(s => s.id != activeSlide.id))
  };

  const editTitle = async (e) => {
    const title = e.target.innerText;

    await post(`slide/edit`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify({
        id: activeSlide.id,
        title: title
      })
    })

    setSlides(slides.map(slide => {
      if(slide.id === activeSlide.id){
        return {
          ...slide,
          title: title
        }
      }
      return slide
    }))
  }

  const editText = async (e) => {
    const text = e.target.innerText;

    await post(`slide/edit`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify({
        id: activeSlide.id,
        text: text
      })
    })

    setSlides(slides.map(slide => {
      if(slide.id === activeSlide.id){
        return {
          ...slide,
          text: text
        }
      }
      return slide
    }))
  }

  return (
    <div className={styles.slideEdit}>
      <div className={styles.slideActions}>
        <svg
          onClick={removeSlide}
          xmlns="http://www.w3.org/2000/svg"
          width="24"
          height="24"
          fill="currentColor"
          className="bi bi-trash-fill"
          viewBox="0 0 16 16"
        >
          <path d="M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1H2.5zm3 4a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 .5-.5zM8 5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7A.5.5 0 0 1 8 5zm3 .5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 1 0z" />
        </svg>
      </div>
      <div className={styles.slideFrame}>
        <img
          className={styles.slideBackground}
          src="https://slideswith.com/cdn-cgi/image/w=1900,h=1400,fit=scale-down,metadata=none,onerror=false/https://slideswith.com//backgrounds/background-20.jpg"
          alt=""
        />
        <div className={styles.content}>
          <h1 onBlur={editTitle} className={styles.title}>{activeSlide.title}</h1>
          <p onBlur={editText} className={styles.text}>{activeSlide.text}</p>
        </div>
      </div>
    </div>
  );
};

export default Main;
