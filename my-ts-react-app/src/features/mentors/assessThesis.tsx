import React, { useEffect, useState } from 'react';
import { observer } from 'mobx-react-lite';
import { Button, Loader, Rating, Message } from 'semantic-ui-react';
import AdministratorStore from '../../app/stores/administratorStore';
import MentorStore from '../../app/stores/mentorStore';
import LayoutWithSidebar from '../../app/layout/LayoutWithSidebar';
import { toast } from 'react-toastify';

const AssessThesis: React.FC = () => {
    const [administratorStore] = useState(() => new AdministratorStore());
    const [mentorStore] = useState(() => new MentorStore());
    const [theses, setTheses] = useState<any>(null);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);
    const [assessmentGrade, setAssessmentGrade] = useState<{ [key: number]: number }>({});
    const [grades, setGrades] = useState<{ [thesisId: number]: number }>({});


    useEffect(() => {
        const loadTheses = async () => {
            try {
                setLoading(true);
                const loadedTheses = await administratorStore.getAllThesis();
                setTheses(loadedTheses);
            } catch (e) {
                setError('Failed to load theses.');
            } finally {
                setLoading(false);
            }
        };
        loadTheses();
    }, [administratorStore]);

    const handleAssess = async (thesisId:number) => {
      const grade = grades[thesisId];
      if (grade === undefined || isNaN(grade)) {
          toast.error('Invalid or no grade provided. Please enter a valid grade.');
          return;
      }
  
      try {
          await mentorStore.assessDiplomaThesis(thesisId, grade);
          toast.success("Diploma thesis assessed successfully");
      } catch (error) {
          toast.error('Failed to assess the thesis. Please try again.');
      }
  };
  

    const handleGradeChange = (thesisId: number, grade: number) => {
      setGrades({ ...grades, [thesisId]: grade });
  };

    return (
        <LayoutWithSidebar>
            <div className="p-4 bg-gray-100">
                {loading ? (
                    <Loader active inline="centered">Loading Thesis...</Loader>
                ) : error ? (
                    <Message negative>
                        <Message.Header>Error</Message.Header>
                        <p>{error}</p>
                    </Message>
                ) : theses && theses.length > 0 ? (
                    <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4">
                        {theses.map((thesis: any) => (
                            <div key={thesis.id} className="bg-white rounded-lg shadow-md overflow-hidden">
                                <div className="p-6">
                                    <h2 className="text-2xl font-semibold mb-2">Thesis #{thesis.id}</h2>
                                    <p className="text-gray-600 mb-4">{thesis.level} - {thesis.titleName}</p>
                                    <div className="mb-4"><strong>Student:</strong> {thesis.studentName}</div>
                                    <div className="mb-4"><strong>Mentor:</strong> {thesis.mentorName}</div>
                                    <input 
                                    type="number" 
                                    placeholder="Enter grade"
                                    value={grades[thesis.id] || ''}
                                    onChange={(e) => handleGradeChange(thesis.id, Number(e.target.value))}
                                    className="input-field-class" // Add your CSS class for styling
                                />
                                <Button 
                                    className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded mt-2"
                                    onClick={() => handleAssess(thesis.id)}
                                >
                                    Assess
                                </Button>
                                </div>
                            </div>
                        ))}
                    </div>
                ) : (
                    <p className="text-center text-gray-600">No thesis available for approval.</p>
                )}
            </div>
        </LayoutWithSidebar>
    );
};

export default observer(AssessThesis);
