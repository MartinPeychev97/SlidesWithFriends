import React from "react";
import { createContext, useState, useEffect } from "react";
import useFetch from "../hooks/useFetch";

const SlideContext = createContext({});

export const SlideProvider = ({ children }) => {
    const [presentation, setPresentation] = useState("");
    const [slides, setSlides] = useState([]);
    const [activeSlide, setActiveSlide] = useState({});
    const [isAddNewSlideOpen, setIsAddNewSlideOpen] = useState(false);
    const presentationId = window.location.href.split("/").pop();

    const { get } = useFetch();

    useEffect(() => {
        const getPresentation = async () => {
            const data = await get(`presentation/getById?id=${presentationId}`);
            setPresentation({
                name: data.name,
                image: data.image
            });
            setSlides(data.slides);
            setActiveSlide(data.slides[0]);
        };

        getPresentation();
    }, []);

    return (
        <SlideContext.Provider
            value={{
                presentation,
                setPresentation,
                slides,
                setSlides,
                activeSlide,
                setActiveSlide,
                presentationId,
                isAddNewSlideOpen,
                setIsAddNewSlideOpen,
            }}
        >
            {children}
        </SlideContext.Provider>
    );
};

export default SlideContext;
