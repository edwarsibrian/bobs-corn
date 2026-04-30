import { useNavigate } from 'react-router';
import Button from './Button';

export default function NotFoundRoute() {
    const navigate = useNavigate();

    return (
        <>
            <div className="custom-bg text-dark">
                <div className="d-flex flex-column align-items-center justify-content-center min-vh-100 px-2">
                    <div className="text-center">
                        <h1 className="display-1 fw-bold">404</h1>
                        <p className="fs-3"> <span className="text-danger">Oops!</span> Page not found.</p>
                        <p className="mt-4 mb-5">The page you&apos;re looking for doesn&apos;t exist.</p>
                        <Button onClick={() => navigate('/')} className="btn btn-light fw-semibold rounded-pill px-4 py-2 custom-btn">
                            Go Home
                        </Button>
                    </div>
                </div>
            </div>
        </>
    );
}