import React, { useEffect, useState } from 'react';
import 'antd/dist/antd.css';
import { Input, Button, Select, Option } from 'antd';
import { useHistory } from 'react-router-dom';
import ReactQuill from 'react-quill';
import 'react-quill/dist/quill.snow.css';
import { postQuestions } from './../Api';
import { postTopic } from './../Api';
import { getTopic } from './../Api';


export const NewQuestion = () => {

    let history = useHistory();

    
    const [title, SetTitle] = useState(localStorage.getItem("questionsTitle"));
    const [textQuestion, SetTextQuestion] = useState("");
    const [selectedTopics, SetSelectedTopics] = useState([]);
    const [topics, SetTopics] = useState([]);
    const { Option } = Select;
    const topicSelect = [];

    useEffect(() => {
        async function fetchData() {
            getTopic().then((result) => {
                SetTopics(result.data);
            });           
        }
        fetchData();
    }, []);


    topics.map((item) => {
        topicSelect.push(<Option key={item}>{item}</Option>)
    });

    const titleChange = (event) => {
        SetTitle(event.target.value);
    };

    const textChange = value => {
        SetTextQuestion(value);
    };

    const cancelQuestion = () => {
        history.replace('/questions');
    };

    function handleChangeTopic(title) {
        postTopic(title);
        selectedTopics.push(title);
        SetSelectedTopics(selectedTopics);
    }

    const saveQuestion = () => {
        
        var item = {
            title: title,
            textQuestion: textQuestion,
            topics: selectedTopics.length === 0 ? [] : selectedTopics[selectedTopics.length - 1],
            answers: [],
            author: localStorage.getItem("userName"),
            likes: [],
            dislikes: []
        };

        postQuestions(item);
    };

    return (<div className="askContainer">
        <span className='title'>Ask a question</span>
        <Input placeholder="What do you want to know?" onChange={titleChange} value={title} />
        <ReactQuill style={{ height: '200px', marginTop: '30px' }} onChange={textChange} value={textQuestion} />
        <div style={{ marginTop: '50px' }} className='topicContainer'>
            <Select
                mode="tags"
                size={topics.length}
                placeholder="Please select topic"
                onChange={handleChangeTopic}
                style={{ width: '100%', textAlign: 'left', marginTop: '20px' }}>
                {topicSelect}
            </Select>
        </div>
        <div>
            <Button style={{ marginTop: '20px', float: 'left' }} type="primary" onClick={saveQuestion}>Save</Button>
            <Button style={{ marginTop: '20px', float: 'left' }} type="text" onClick={cancelQuestion}>Cancel</Button>
        </div>
    </div>);
   
}