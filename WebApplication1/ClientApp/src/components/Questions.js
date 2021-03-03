import React, { useState, useEffect } from 'react';
import { getQuestions, getQuestionsByTopic, getQuestionsByFilter } from './../Api';
import { QuestionItem } from './QuestionItem';
import { Input, Button } from 'antd';
import { useHistory, useParams } from 'react-router-dom';

export const Questions = () => {

    let history = useHistory();
    const { top }  = useParams();
    const { filter }  = useParams();

    const [title, SetTitle] = useState("");
    const [questions, SetQuestions] = useState([]);

    useEffect(() => {
        async function fetchData() {

            if (top !== undefined) {
                getQuestionsByTopic(top).then((result) => {
                    SetQuestions(result.data);
                });
            }
            else if (filter !== undefined) {
                getQuestionsByFilter(filter).then((result) => {
                    SetQuestions(result.data);
                });
            }
            else
                getQuestions().then((result) => {
                    SetQuestions(result.data);
                });
        }
        fetchData();
    }, [top, filter]);


    const titleChange = (event) => {
        SetTitle(event.target.value);
    };

    function saveTitle() {
        localStorage.setItem("questionsTitle", title);
        history.replace('/newquestion');
    }

    function popularFilter() {
        history.replace('/questions/popular');
    }

    function recentFilter() {
        history.replace('/questions/recent');
    }

    function unansweredFilter() {
        history.replace('/questions/unanswered');
    }

    return (<div>
        <div className="askContainer">
            <span className='title'>Questions</span>
            <div className='block main'>
                <div className='card' style={{ width: '800px' }}>
                    <Input placeholder="What do you want to know?" onChange={titleChange} value={title}></Input>
                    <Button type="primary" onClick={saveTitle}>Ask Question</Button>
                </div>
            </div>
            <div className='card' style={{ marginTop: '10px' }}>
                <Button onClick={popularFilter}>Popular</Button>
                <Button style={{ marginLeft: '10px' }} onClick={recentFilter}>Recent</Button>
                <Button style={{ marginLeft: '10px' }} onClick={unansweredFilter}>Unanswered</Button>
            </div>
        </div>
        {questions.map((item) => <QuestionItem value={item} question={item} key={item.title}></QuestionItem>)}
    </div>);
}