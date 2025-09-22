import React, { useState } from "react";
import { MusicContract } from "../types/musicContract ";

const Main: React.FC = () => {
  const [musicFile, setMusicFile] = useState<File | null>(null);
  const [partnerFile, setPartnerFile] = useState<File | null>(null);
  const [partner, setPartner] = useState("");
  const [date, setDate] = useState("");
  const [results, setResults] = useState<MusicContract[]>([]);
  const [error, setError] = useState<string>("");

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!musicFile || !partnerFile || !partner || !date) {
      setError("Fields are required, please check");
      return;
    }

    const formData = new FormData();
    formData.append("MusicContracts", musicFile);
    formData.append("PartnerContracts", partnerFile);
    formData.append("Partner", partner);
    formData.append("Date", date);

    try {
      setError("");

      const res = await fetch(`https://localhost:7088/contracts/upload`, {
        method: "POST",
        body: formData,
      });

      if (!res.ok) {
        const errData = await res.json();
        setError(errData.error || "Internal server error");
        setResults([]);
        return;
      }

      const data: MusicContract[] = await res.json();
      setResults(data);
    } catch (err: any) {
      setError(err.message);
      setResults([]);
    }
  };

  return (
    <div style={{ padding: "2rem" }}>
      <h1>Global Rights Management</h1>
      <form onSubmit={handleSubmit}>
        <div>
          <label data-testid="musicContracts">Music Contracts File:</label>
          <input
            id="musicContracts"
            type="file"
            onChange={(e) => setMusicFile(e.target.files?.[0] || null)}
          />
        </div>
        <div>
          <label data-testid="partnerContracts">Partner Contracts File:</label>
          <input
            id="partnerContracts"
            type="file"
            onChange={(e) => setPartnerFile(e.target.files?.[0] || null)}
          />
        </div>
        <div>
          <label data-testid="partner">Partner:</label>
          <input
            id="partner"
            type="text"
            value={partner}
            onChange={(e) => setPartner(e.target.value)}
          />
        </div>
        <div>
          <label data-testid="date">Date:</label>
          <input
            id="date"
            type="date"
            value={date}
            onChange={(e) => setDate(e.target.value)}
          />
        </div>
        <button type="submit">Find contracts</button>
      </form>

      {error && <p style={{ color: "red" }}>{error}</p>}

      {results.length > 0 && (
        <table
          border={1}
          cellPadding={5}
          style={{ marginTop: "1rem", borderCollapse: "collapse" }}
        >
          <thead>
            <tr>
              <th>Artist</th>
              <th>Title</th>
              <th>Usages</th>
              <th>Start Date</th>
              <th>End Date</th>
            </tr>
          </thead>
          <tbody>
            {results.map((c, idx) => (
              <tr key={idx}>
                <td>{c.artist}</td>
                <td>{c.title}</td>
                <td>{c.usages.join(", ")}</td>
                <td>{new Date(c.startDate).toLocaleDateString()}</td>
                <td>{c.endDate ? new Date(c.endDate).toLocaleDateString() : ""}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
};

export default Main;
