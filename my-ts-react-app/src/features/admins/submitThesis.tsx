import React, { useState, useEffect } from 'react';
import { observer } from 'mobx-react-lite';
import { Button, Loader } from 'semantic-ui-react';
import AdministratorStore from '../../app/stores/administratorStore';
import LayoutWithSidebar from '../../app/layout/LayoutWithSidebar';

const SubmitThesis: React.FC = () => {
    const [administratorStore] = useState(() => new AdministratorStore());
    const [theses, setTheses] = useState<any[]>([]);
    const [loading, setLoading] = useState<boolean>(true);

    const formatDate = (dateString: any) => {
        const options = { year: 'numeric', month: '2-digit', day: '2-digit', hour: '2-digit', minute: '2-digit' };
        return new Date(dateString).toLocaleString(undefined).replace(',', ' ');
    };

    useEffect(() => {
        const loadTheses = async () => {
            try {
                setLoading(true); 
                const loadedTheses = await administratorStore.getAllThesis();

                if (loadedTheses) {
                    const approvedTheses = loadedTheses.filter((thesis) => thesis.dueDate);
                    setTheses(approvedTheses);
                }
            } catch (e) {
                console.error('Failed to load approved theses:', e);
            } finally {
                setLoading(false); 
            }
        };
        loadTheses();
    }, [administratorStore]);

    const handleThesisSubmission = async (thesis: any) => {
        try {
            if (thesis.dueDate !== null && !isThesisSubmitted(thesis.id)) {
                await administratorStore.submitThesisApplication(thesis.id);

                localStorage.setItem(`thesis_${thesis.id}_submitted`, 'true');

                setTheses((prevTheses) =>
                    prevTheses.map((prevThesis) =>
                        prevThesis.id === thesis.id
                            ? { ...prevThesis, isSubmitted: true, submissionDate: new Date().toLocaleString() }
                            : prevThesis
                    )
                );
            }
        } catch (error) {
            console.error('Error occurred while submitting the thesis application:', error);
        }
    };

    const isThesisSubmitted = (thesisId: number) => {
        
        return localStorage.getItem(`thesis_${thesisId}_submitted`) === 'true';
    };

    return (
        <LayoutWithSidebar>
            <div className="p-4 bg-gray-100">
                {loading ? ( 
                    <Loader active inline="centered">
                        Loading Thesis...
                    </Loader>
                ) : theses && theses.length > 0 ? (
                    <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4">
                        {theses.map((thesis: any) => (
                            <div key={thesis.id} className="bg-white rounded-lg shadow-md overflow-hidden">
                                <div className="p-6">
                                    <h2 className="text-2xl font-semibold mb-2">Thesis #{thesis.id}</h2>
                                    <p className="text-gray-600 mb-4">
                                        {thesis.level} - {thesis.titleName}
                                    </p>
                                    <div className="mb-4">
                                        <strong>Student:</strong> {thesis.studentName}
                                    </div>
                                    <div>
                                        <strong>Mentor:</strong> {thesis.mentorName}
                                    </div>
                                </div>
                                <div className="p-4 bg-gray-200 border-t border-gray-300">
                                    {isThesisSubmitted(thesis.id) ? (
                                        <div className="text-center text-gray-500 font-semibold">
                                            {`Submitted at ${formatDate(thesis.submissionDate)}`}
                                        </div>
                                    ) : (
                                        <Button
                                            onClick={() => handleThesisSubmission(thesis)}
                                            className={`w-full bg-blue-500 hover:bg-blue-600 text-white rounded-full py-2`}
                                            disabled={thesis.submissionStatus === 'Thesis is not approved for submission'}
                                        >
                                            {thesis.isSubmitted ? `Submitted at ${formatDate(thesis.submissionDate)}` : 'Submit Thesis'}
                                        </Button>
                                    )}
                                </div>
                            </div>
                        ))}
                    </div>
                ) : (
                    <p className="text-center text-gray-600">No theses available for submission.</p>
                )}
            </div>
        </LayoutWithSidebar>
    );
};

export default observer(SubmitThesis);
