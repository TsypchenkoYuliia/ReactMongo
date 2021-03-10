import React, { useState, useEffect  } from 'react';
import { useParams, useHistory } from "react-router-dom";
import { UpCircleOutlined, DownCircleOutlined } from '@ant-design/icons';
import { Typography, Button } from 'antd';
import { CommentOutlined } from '@ant-design/icons';
import { getQuestionById, postAnswer, getAnswers, postLike, postDislike, getVotes } from './../Api';
import Moment from 'moment';
import ReactQuill from 'react-quill';
import 'react-quill/dist/quill.snow.css';

export const Question = () => {

    let { id } = useParams();
    let history = useHistory();

    const { Text } = Typography;
    const [textAnswer, SetTextAnswer] = useState("");
    const [question, SetQuestion] = useState({});
    const [answers, SetAnswers] = useState(question.answers);
    const [likes, SetLikes] = useState(0);
    
    useEffect(() => {
        async function fetchData() {
            getQuestionById(id).then((result) => {
                SetQuestion(result.data);
            });
        }
        fetchData();
    }, [id, question]);

    //useEffect(() => {
    //    async function fetchData() {
    //        getVotes(id).then((result) => {
    //            SetLikes(result.data);
    //        });
    //    }
    //    fetchData();
    //}, [likes]);

    //useEffect(() => {
    //    async function fetchData() {
    //        getAnswers(id).then((result) => {
    //            SetAnswers(result.data);
    //        });
    //    }
    //    fetchData();
    //}, [answers]);

    const textChange = value => {
        SetTextAnswer(value);
    };

    const saveAnswer = () => {
        var item = {
            text: textAnswer,
            author: localStorage.getItem("userName"),
        };
        SetTextAnswer("");
        postAnswer(id, item);

        //SetAnswers([]);
    };

    const addLike = () => {
        postLike(id, { author: localStorage.getItem("userName") })
        SetLikes(0);
    }

    function addDislike() {
        postDislike(id, { author: localStorage.getItem("userName") })
        SetLikes(0);
    }

    function addLikeAnswer(answerId) {

        
    }

    function addDislikeAnswer(answerId) {

       
    }

    function editQuestion() {
       
    }

    function deleteQuestion() {
       
    }

    let count = 0;


    return <div className="askContainer" style={{ width: '800px' }}>
        <Text style={{ fontSize: '15px', fontSize: '25px', fontWeight: '400' }}>Answers to the question:</Text>
        <div className='card'>
            <div className='info' style={{ marginTop: '30px', marginLeft: '30px' }}>
                <div><UpCircleOutlined style={{ fontSize: '25px' }} onClick={addLike} /></div>
                <div style={{ fontSize: '30px', marginTop: '10px' }}>{likes}</div>
                <div><DownCircleOutlined style={{ fontSize: '25px', marginTop: '10px' }} onClick={addDislike} /></div>
            </div>
            <div className='info' style={{ marginTop: '25px', marginLeft: '30px' }}>
                <div className='titleContainer'>
                    <Text style={{ marginLeft: '10px', fontSize: '15px', fontWeight: '400', color: 'blue' }}>Author: {question.author}</Text>
                    <Text style={{ marginLeft: '10px', fontSize: '15px', fontWeight: '400', color: 'green' }}>{Moment(question.date).format('DD/MM/YYYY')}</Text>
                </div>
                <Text style={{ fontSize: '15px', fontWeight: '300', marginTop: '15px', textAlign: 'left' }}>{question.text}</Text>
            </div>
        </div>
        <div style={{ marginRight: 'auto', marginLeft: '100px', marginTop: '15px' }}>
            <CommentOutlined style={{ fontSize: '20px' }} /> Comment
            <span style={{ fontSize: '20px', marginLeft: '10px', marginRight: '5px' }}>&bull;</span>
            <Button type="text" onClick={editQuestion}>Edit</Button>
            <span style={{ fontSize: '20px', marginLeft: '5px', marginRight: '5px' }}>&bull;</span>
            <Button type="text" onClick={deleteQuestion}>Delete</Button>
        </div>
        <ColoredLine color="#eceaea" />
        <div>
            {question.answers != null ? question.answers.map(item => {
                return <div className='answerContainer' style={{ marginTop: '15px' }}>
                    <div >
                        <div>
                            <Text style={{ fontSize: '15px', fontWeight: '400', width: '100px' }}>Answer&bull;{++count}</Text>
                            <div>
                                <UpCircleOutlined style={{ fontSize: '15px' }} onClick={() => addLikeAnswer(item.id)} />
                                <div style={{ fontSize: '15px' }}>{item.likes?.length - item.dislikes?.length}</div>
                                <DownCircleOutlined style={{ fontSize: '15px' }} onClick={() => addDislikeAnswer(item.id)} /></div>
                        </div>
                    </div>
                    <div className='info'>
                        <div className='titleContainer'>
                            <Text style={{ marginTop: '25px', marginLeft: '50px', fontSize: '15px', fontWeight: '400', color: 'blue' }}>{item.author}</Text>
                            <Text style={{ marginTop: '25px', marginLeft: '50px', fontSize: '15px', fontWeight: '400', color: 'green' }}>{Moment(item.date).format('DD/MM/YYYY')}</Text>
                        </div>
                        <Text style={{ marginLeft: '50px', fontSize: '15px', fontWeight: '400', textAlign: 'left' }}>{item.text}</Text>
                        <div>
                            <div style={{ marginTop: '15px', float: 'left' }}>
                                <CommentOutlined style={{ fontSize: '20px' }} /> Comment
                                <span style={{ fontSize: '20px', marginLeft: '15px' }}>&bull;</span>
                                <Button style={{ marginLeft: '2px' }} type="text">Assept answer</Button>
                            </div>
                        </div>
                        <ColoredAnswerLine color="#eceaea" />
                    </div>
                </div>
                { count = 0 }
            }) : <div></div>}
        </div>
        <div className='block' style={{ marginTop: '20px', width: '900px' }}>
            <Text style={{ marginRight: 'auto', marginLeft: '50px', fontSize: '25px', fontWeight: '400' }}>Your answer:</Text>
            <ReactQuill style={{ height: '200px', marginTop: '30px' }} onChange={textChange} value={textAnswer} />
        </div>
        <Button style={{ marginTop: '70px', float: 'left', width: '100px', marginBottom: '50px' }} type="primary" onClick={saveAnswer}>Add answer</Button>
    </div>
}

const ColoredLine = ({ color }) => (
    <hr
        style={{
            color: color,
            height: 2,
            width: 900
        }}
    />
);

const ColoredAnswerLine = ({ color }) => (
    <hr
        style={{
            color: color,
            height: 2,
            width: 900,
            marginLeft: -65
        }}
    />
);