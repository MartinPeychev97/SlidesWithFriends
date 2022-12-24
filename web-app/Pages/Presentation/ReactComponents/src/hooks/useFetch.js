import { useEffect, useState } from "react";

const useFetch = () => {
  const base = window.location.origin;

  const get = async (url) => {
    let result;

    try {
      const response = await fetch(`${base}/${url}`);
      const data = await response.json();
      result = data;
    } catch (error) {
      console.log(error);
    }

    return result;
  }

  const post = async (url, options) => {
    let result;

    try {
      const response = await fetch(`${base}/${url}`, options);
      const data = await response.json();
      result = data;
    } catch (error) {
      console.log(error);
    }

    return result;
  }

  return { get, post };
};

export default useFetch;
