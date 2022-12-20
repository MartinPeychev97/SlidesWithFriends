import React from "react";
import { createContext, useState, useEffect } from "react";
import useFetch from "../hooks/useFetch";

const SlideContext = createContext({});

export const SlideProvider = ({ children }) => {
  const [presentationName, setPresentationName] = useState("");
  const [slides, setSlides] = useState([]);
  const [activeSlide, setActiveSlide] = useState({});
  const presentationId = window.location.href.split("/").pop();

  const { get } = useFetch();

  useEffect(() => {
    const getPresentation = async () => {
      const data = await get(`presentation/getById?id=${presentationId}`);
      setPresentationName(data.name)
      setSlides(data.slides);
      setActiveSlide(data.slides[0])
    }

    getPresentation();
  }, []);


  return (
    <SlideContext.Provider
      value={{presentationName, setPresentationName, slides, setSlides, activeSlide, setActiveSlide, presentationId }}
    >
      {children}
    </SlideContext.Provider>
  );
};

export default SlideContext;
