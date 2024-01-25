import React from 'react';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import { useNavigate } from 'react-router-dom';
import { useStore } from '../../app/stores/store';
import { LoginDTO } from '../../app/models/DTOS/LoginDTO';
import Spinner from '../../app/common/Spinner';


const validationSchema = Yup.object({
  Email: Yup.string().email('Invalid email format').required('Email is required'),
  Password: Yup.string().required('Password is required'),
});

const LoginForm = () => {
  const { authenticationStore } = useStore();
  const navigate = useNavigate();

  

  return (
    <div className="flex flex-col items-center justify-center h-screen bg-gray-100">
      
      <h1 className="text-3xl font-semibold text-gray-800 mb-6">Login</h1>
      <Formik
        initialValues={{
          Email: '',
          Password: '',
        }}
        validationSchema={validationSchema}
        onSubmit={(values, { setSubmitting }) => {
          const loginData: LoginDTO = {
              email: values.Email,
              password: values.Password,
          };
          setSubmitting(true); 
          authenticationStore.login(loginData)
              .then(() => {
                  navigate('/homepage', { replace: true });
              })
              .catch((error) => {
                  console.error('Login Error:', error);
              })
              .finally(() => setSubmitting(false));
      }}
      
      >
        {({ isSubmitting }) => (
          <Form className="bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4 w-full max-w-sm">
            <div className="mb-4">
              <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="Email">
                Email
              </label>
              <Field name="Email" type="email" placeholder="Email" className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" />
              <ErrorMessage name="Email" component="div" className="text-red-500 text-xs italic" />
            </div>

            <div className="mb-6">
              <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="Password">
                Password
              </label>
              <Field name="Password" type="password" placeholder="Password" className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" />
              <ErrorMessage name="Password" component="div" className="text-red-500 text-xs italic" />
            </div>

            <div className="flex items-center justify-between">
            <button type="submit" disabled={isSubmitting} className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline">
                {isSubmitting ? 'Logging in...' : 'Login'}
              </button>
              {/* Add additional elements like "Forgot password?" link if necessary */}
            </div>
          </Form>
        )}
      </Formik>
    </div>
  );
};

export default LoginForm;
