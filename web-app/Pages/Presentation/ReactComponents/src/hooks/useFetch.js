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

    const put = async (url, data) => {
        let result;
        try {
            const response = await fetch(`${base}/${url}`, createOptions('PUT', data));
            result = await response.json();
        } catch (error) {
            setError(error);
        }

        return result;
    }

    const createOptions = (method = 'GET', data) => {
        const result = {
            method,
            headers: {}
        }

        if (data) {
            result.headers['Content-Type'] = 'application/json';
            result.body = JSON.stringify(data);
        }

        return result;
    }


    return { get, post, put };
};

export default useFetch;
