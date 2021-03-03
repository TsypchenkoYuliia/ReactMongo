import React, { useState } from 'react';
import 'antd/dist/antd.css';
import { Input, Button } from 'antd';
import { useHistory } from 'react-router-dom';

export const Login = () => {

    let history = useHistory();

    const [userName, SetUserName] = useState("");

    function changeName(event) {
        SetUserName(event.target.value);
    }

    function saveName() {
        localStorage.setItem('userName', userName);

        if (userName !== "")
            history.replace('/questions');
    }

    return <div className='nameContainer'>
        <Input
            value={userName}
            onChange={changeName}
            style={{ margin: '10px' }}
            placeholder="Your name:" />
        <Button type="primary"
            onClick={saveName}
            style={{ margin: 'auto', width: '100px' }}>Login</Button>
    </div>;

    //export default Login;
}



