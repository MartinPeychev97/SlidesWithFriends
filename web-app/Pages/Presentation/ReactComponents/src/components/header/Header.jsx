import React, { useContext } from "react";
import SlideContext from "../../context/SlideContext";
import useFetch from "../../hooks/useFetch";
import styles from "./header.module.css";

const Header = () => {
  const { presentationName, setPresentationName, presentationId} = useContext(SlideContext);
  const { post } = useFetch();

  const handleChange = (e) => {
    setPresentationName(e.target.value)
    console.log(presentationId)
  }

  const editName = async (e) => {
    const name = e.target.value;

    await post(`presentation/editName`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify({
        id: +presentationId,
        name: name
      })
    })
  }

  return (
    <header className={styles.header}>
      <div className={styles.slideActionsLeft}>
        <input onBlur={editName} onChange={handleChange} type="text" value={presentationName} />
      </div>
    </header>
  );
};

export default Header;
