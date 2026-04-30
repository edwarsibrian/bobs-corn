import { Link } from "react-router";

export default function Home() {
    return (
        <section className="text-center">
            <h1 className="display-5 fw-bold">
                <img
                    src="/assets/corn1.png"
                    alt="Logo"
                    className="brand-logo"
                />
                Welcome to Bob&apos;s Corn
                <img
                    src="/assets/corn1.png"
                    alt="Logo"
                    className="brand-logo"
                />
            </h1>
            <p className="lead text-muted">
                Buy up to one corn per minute.
            </p>

            <Link to="/corns-purchased" className="btn btn-primary fw-semibold rounded-pill px-4 py-2 custom-btn">
                Start Buying Corn
            </Link>
        </section>
    );
}