import axios from 'axios';

const BASE_URL = 'https://localhost:44317/api/';

const axiosApi = axios.create({
    baseURL: BASE_URL,
});

export const postQuestions = (question) => {
    return axiosApi.post(`question/`, question);
};

export const postTopic = (topics) => {
    return axiosApi.post(`topic/`, topics);
};

export const getTopic = () => {
    return axiosApi.get(`topic/`);
};

export const getQuestions = () => {
    return axiosApi.get(`question/`);
};

export const getQuestionsByTopic = (topic) => {
    return axiosApi.get(`question/topic/${topic}`);
}

export const getQuestionsByFilter = (filter) => {

}