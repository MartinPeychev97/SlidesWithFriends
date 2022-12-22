import React, { useContext } from "react";
import SlideContext from "../../context/SlideContext";
import useFetch from "../../hooks/useFetch";
import styles from "./main.module.css";

const Main = () => {
  const { activeSlide, slides, setSlides, setIsAddNewSlideOpen } = useContext(SlideContext);
  const { post } = useFetch();

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

  const closeAddNewSlide = () => {
    setIsAddNewSlideOpen(false)
  }

  return (
    <div onClick={closeAddNewSlide} className={styles.slideEdit}>
      <div className={styles.slideFrame}>
        <img
          className={styles.slideBackground}
          src={`data:image/jpeg;base64,${activeSlide.image}`}
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
