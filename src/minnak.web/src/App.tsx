import React, { useState } from 'react';
import logo from './logo.svg';
import './App.css';
import { request } from 'http';
import { json } from 'stream/consumers';

function App() {
  const [formData , setFormData] = useState({
    url: "",
    alias:""
  })
  const [result, setResult] = useState({
    resultURL : "Size doesn't matter it redirects anyways!!"
  })
  const onChange = (e : any) => setFormData({...formData,[e.target.id]: e.target.value});
  const onSubmit = (e : any) => {
    e.preventDefault();
    console.log(formData);


    fetch('https://localhost:49153/api/URL/Shorten?url=' + formData.url + '&alias=' + formData.alias ,
            {
                method: "POST"
            })
            .then((response: any) => response.text())
            .then(resultURL => {
              console.log(resultURL);
              setResult({...result,resultURL});
            });

  }
  return (
    <div>
      <form onSubmit={(e) => onSubmit(e)} className="container mt-5">
        <div className="mb-3">
            <label htmlFor="url" className="form-label">URL</label>
            <input type="text" onChange={e => onChange(e)} className="form-control" id="url" placeholder="minnakURL.it"/>
        </div>
        <div className="mb-3">
            <label htmlFor="alias" className="form-label">Alias</label>
            <input type="text" onChange={e => onChange(e)}  className="form-control" id="alias"/>
        </div>
        <div className='mb-3'>
            <input type={'submit'}  className='btn btn-lg' id='submitbutton'/>
        </div>
        <div className="mb-3">
            <label htmlFor="shortenedURL" className="form-label">Shortened URLs</label>
            <textarea disabled className="form-control" id="shortenedURL" rows={3}  value={result.resultURL}></textarea>
        </div>
      </form>
    </div>
  );
}


export default App;
