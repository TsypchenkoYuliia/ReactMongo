import React, { useState } from 'react';
import { useParams, useHistory } from "react-router-dom";
import { UpCircleOutlined, DownCircleOutlined } from '@ant-design/icons';
import { Select, Typography, Input, Button, Alert } from 'antd';
import { CommentOutlined } from '@ant-design/icons';
import { UpdateCollection, GetCurrentUser, GetCollection } from './../data';
import Moment from 'moment';
import ReactQuill from 'react-quill';
import 'react-quill/dist/quill.snow.css';

export const Question = () => {

    let { id } = useParams();
    let history = useHistory();

    const { Text } = Typography;
    const [textAnswer, SetTextAnswer] = useState("");

}