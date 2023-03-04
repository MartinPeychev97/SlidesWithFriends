import React, { useContext, useState } from "react";
import { ChromePicker } from 'react-color';
import styles from './color-picker.module.css';
import useFetch from '../../hooks/useFetch';
import SlideContext from "../../context/SlideContext";

const ColorPicker = () => {
    const { slides, setSlides, activeSlide, setActiveSlide } = useContext(SlideContext);

    const [color, setColor] = useState('');
    const [isVisible, setVisiblity] = useState(false);
    const { put } = useFetch();

    const [delayed, setDelayed] = useState(null);

    async function changeColor(updatedColor) {
        const color = updatedColor.hex;
        setColor(color);

        setActiveSlide({
            ...activeSlide,
            background: color
        })

        if (delayed != null) clearTimeout(delayed);
        setDelayed(setTimeout(function () {
            put('presentation/editbackground', data);
        }, 1000));

        setSlides(slides.map(s => {
            if (s.id === activeSlide.id) {
                return {
                    ...s,
                    background: color,
                }
            }
            return s;
        }))

        const data = {
            id: +activeSlide.id,
            background: color
        }

        await put('slide/editbackground', data);
    }

    function toggle() {
        setVisiblity(!isVisible);
    }

    return (
        <React.Fragment>
            <svg
                onClick={toggle}
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 512 512"
                width="20"
                fill="currentColor"
            >
                <path d="M512 256c0 .9 0 1.8 0 2.7c-.4 36.5-33.6 61.3-70.1 61.3H344c-26.5 0-48 21.5-48 48c0 3.4 .4 6.7 1 9.9c2.1 10.2 6.5 20 10.8 29.9c6.1 13.8 12.1 27.5 12.1 42c0 31.8-21.6 60.7-53.4 62c-3.5 .1-7 .2-10.6 .2C114.6 512 0 397.4 0 256S114.6 0 256 0S512 114.6 512 256zM128 288c0-17.7-14.3-32-32-32s-32 14.3-32 32s14.3 32 32 32s32-14.3 32-32zm0-96c17.7 0 32-14.3 32-32s-14.3-32-32-32s-32 14.3-32 32s14.3 32 32 32zM288 96c0-17.7-14.3-32-32-32s-32 14.3-32 32s14.3 32 32 32s32-14.3 32-32zm96 96c17.7 0 32-14.3 32-32s-14.3-32-32-32s-32 14.3-32 32s14.3 32 32 32z" />
            </svg>
            <div className={styles.colorPicker}>
                {isVisible && (<ChromePicker color={color} onChange={changeColor}></ChromePicker>)}
            </div>
        </React.Fragment>
    )
}

export default ColorPicker;
