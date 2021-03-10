import axios from 'axios';

const BASE_URL = 'https://localhost:44317/api/';

const axiosApi = axios.create({
    baseURL: BASE_URL,
});

export const postQuestions = (question) => {
    return axiosApi.post(`question/`, question);
};

export const postLike = (questionId, like) => {
    return axiosApi.post(`question/like/${questionId}`, like);
};

export const postDislike = (questionId, dislike) => {
    return axiosApi.post(`question/dislike/${questionId}`, dislike);
};

export const getVotes = (questionId) => {
    return axiosApi.get(`question/votes/${questionId}`);
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

export const getQuestionsByFilter = (rating) => {
    return axiosApi.get(`question/rating/${rating}`);
}

export const getQuestionById = (id) => {
    return axiosApi.get(`question/getbyid/${id}`);
}

export const postAnswer = (questionId, answer) => {
    return axiosApi.post(`answer/${questionId}`, answer);
}

export const getAnswers = (questionId) => {
    return axiosApi.get(`answer/${questionId}`);
}