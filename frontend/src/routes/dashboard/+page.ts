export const load = async ({ fetch, data }) => {
  const {token, user} = data;

  const fetchMetrics = async () => {
    const res = await fetch("http://127.0.0.1:5096/api/dashboard/aggregate", {
      headers: { Authorization: `Bearer ${token}` },
    });

    if (!res.ok) throw new Error("Failed to load metrics");

    return await res.json();
  };

  const fetchTransactions = async () => {
    const res = await fetch("http://127.0.0.1:5096/api/transactions", {
      headers: { Authorization: `Bearer ${token}` },
    });

    if (!res.ok) throw new Error("Failed to load transactions");

    const response = await res.json();

    return response.transactions;
  };

  const fetchBreakdown = async () => {
    const res = await fetch("http://127.0.0.1:5096/api/transactions/breakdown", {
      headers: { Authorization: `Bearer ${token}` },
    });

    if (!res.ok) throw new Error("Failed to load breakdown");

    const response = await res.json();

    return response;
  };

  return {
    user,
    streamed: {
      summary: fetchMetrics(),
      transactions: fetchTransactions(),
      breakdown: fetchBreakdown(),
    },
  };
};
