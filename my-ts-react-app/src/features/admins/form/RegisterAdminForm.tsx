import React from 'react';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import { useStore } from '../../../app/stores/store';

const validationSchema = Yup.object({
  Name: Yup.string().required('Name is required'),
  Surname: Yup.string().required('Surname is required'),
  Dob: Yup.date().required('Date of Birth is required'),
  Email: Yup.string().email('Invalid email address').required('Email is required'),
  Password: Yup.string().min(6, 'Password must be at least 6 characters').required('Password is required'),
  Gender: Yup.string().required('Gender is required'),
  Phone: Yup.string().required('Phone is required'),
  Address: Yup.string().required('Address is required'),
  Type: Yup.string().required('Type is required'),
});

const RegisterAdminForm = () => {
  const { userStore } = useStore();

  return (
    <div className="flex items-center justify-center h-screen bg-gray-200">
    <div className="w-full max-w-xl">
      <h2 className="text-center text-3xl font-extrabold text-gray-900 mb-6">Register</h2>
      
    <Formik
      initialValues={{
        Name: '',
        Surname: '',
        Dob: '',
        Email: '',
        Password: '',
        Gender: '',
        Phone: '',
        Address: '',
        Type: 'Admin', 
      }}
      validationSchema={validationSchema}
      onSubmit={(values, actions) => {
        const formattedValues = {
          ...values,
          Dob: new Date(values.Dob)
        };
        console.log('Formatted Values:', formattedValues);

        userStore.registerAdmin(formattedValues)
        
          .then(() => {
            actions.resetForm();
          })
          .catch(error => {
            actions.setFieldError('general', error.message);
          })
          .finally(() => actions.setSubmitting(false));
      }}
    >
     {({ isSubmitting }) => (
            <Form className="bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4">
            <div className="mb-4">
              <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="Name">
                Name
              </label>
              <Field name="Name" type="text" placeholder="Name" className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" />
              <ErrorMessage name="Name" component="div" className="text-red-500 text-xs italic" />
            </div>

            <div className="mb-4">
              <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="Surname">
                Surname
              </label>
              <Field name="Surname" type="text" placeholder="Surname" className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" />
              <ErrorMessage name="Surname" component="div" className="text-red-500 text-xs italic" />
            </div>
            
            <div className="mb-4">
              <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="Dob">
                Date of Birth
              </label>
              <Field name="Dob" type="date" className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" />
              <ErrorMessage name="Dob" component="div" className="text-red-500 text-xs italic" />
            </div>

            <div className="mb-4">
              <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="Email">
                Email
              </label>
              <Field name="Email" type="emil" placeholder="Email" className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" />
              <ErrorMessage name="Email" component="div" className="text-red-500 text-xs italic" />
            </div>

            <div className="mb-4">
              <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="Password">
                Password
              </label>
              <Field name="Password" type="password" placeholder="Password" className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" />
              <ErrorMessage name="Password" component="div" className="text-red-500 text-xs italic" />
            </div>

          <div className="mb-4">
              <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="Gender">
                Gender
              </label>
              <Field name="Gender" as="select" className="shadow border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline">
                <option value="">Select Gender</option>
                <option value="Male">Male</option>
                <option value="Female">Female</option>
              </Field>
              <ErrorMessage name="Gender" component="div" className="text-red-500 text-xs italic" />
            </div>

            <div className="mb-4">
              <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="Phone">
                Phone
              </label>
              <Field name="Phone" type="tel" placeholder="Phone" className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" />
              <ErrorMessage name="Phone" component="div" className="text-red-500 text-xs italic" />
            </div>

            <div className="mb-4">
              <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="Address">
                Address
              </label>
              <Field name="Address" type="text" placeholder="Address" className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" />
              <ErrorMessage name="Address" component="div" className="text-red-500 text-xs italic" />
            </div>

          <Field name="Type" type="hidden" />

          <div className="flex items-center justify-between">
              <button type="submit" disabled={isSubmitting} className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline">
                Register
              </button>
            </div>

            <ErrorMessage name="general" component="div" className="text-center text-red-500 text-xs italic mt-4" />
          </Form>
      )}
    </Formik>
    </div>
  </div>
  );
};

export default RegisterAdminForm;
