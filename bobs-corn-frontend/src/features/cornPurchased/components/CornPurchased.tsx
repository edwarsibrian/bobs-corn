import { useState } from 'react';
import { useForm } from "react-hook-form";
import Button from '../../../components/Button';
import { buyCorn } from '../services/cornPurchaseService';

type CornPurchaseForm = {
    confirmPurchase: boolean;
};

export default function CornPurchased() {

    const [totalCornBought, setTotalCornBought] = useState(0);
    const [message, setMessage] = useState("");
    const [retryAfterSeconds, setRetryAfterSeconds] = useState<number | null>(null);

    const {
        handleSubmit,
        formState: { isSubmitting },
    } = useForm<CornPurchaseForm>();

    const onSubmit = async () => {
        setMessage("");
        setRetryAfterSeconds(null);

        const result = await buyCorn();

        setMessage(result.message);

        if (result.success) {
            setTotalCornBought(result.totalCornBought);
        }
    };

    return (
        <section className="row justify-content-center">
            <div className="col-md-6">
                <div className="card shadow-sm">
                    <div className="card-body text-center">
                        <h1 className="h3 mb-3">Buy Corn</h1>

                        <p className="text-muted">
                            Bob allows each client to buy at most one corn per minute.
                        </p>

                        <form onSubmit={handleSubmit(onSubmit)}>
                            <Button
                                type="submit"
                                className="btn btn-warning fw-semibold px-4"
                                disabled={isSubmitting}
                            >
                                {isSubmitting ? "Buying..." : "Buy Corn"}
                            </Button>
                        </form>

                        <hr />

                        <p className="fs-5">
                            Corn successfully bought:{" "}
                            <strong>{totalCornBought}</strong>
                        </p>

                        {message && (
                            <div
                                className={`alert mt-3 ${retryAfterSeconds ? "alert-danger" : "alert-success"}`}
                            >
                                {message}

                                {retryAfterSeconds && (
                                    <div className="mt-2">
                                        Try again in {retryAfterSeconds} seconds.
                                    </div>
                                )}
                            </div>
                        )}
                    </div>
                </div>
            </div>
        </section>
    );
}
