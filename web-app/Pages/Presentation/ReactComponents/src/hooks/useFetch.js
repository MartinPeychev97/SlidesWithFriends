import { useEffect, useState } from "react";

const useFetch = () => {
  const base = window.location.origin;

  const [error, setError] = useState(null);
  const [isLoading, setIsLoading] = useState(true);

  const get = async (url) => {
    let result;

    try {
      const response = await fetch(`${base}/${url}`);
      const data = await response.json();
      result = data;
    } catch (error) {
      setError(error);
    } finally {
      setIsLoading(false);
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
      setError(error);
    } finally {
      setIsLoading(false);
    }

    return result;
  }

  return { error, isLoading, get, post };
};

export default useFetch;
