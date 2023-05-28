import React, { useEffect, useState } from 'react';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import Box from '@mui/material/Box';
import Container from '@mui/material/Container';
import {  useParams } from 'react-router-dom';
import { Merchandise } from './models/merchandise';
import { Alert, AlertTitle, Typography } from '@mui/material';
import { useCookies } from 'react-cookie';

export function EditMerchandise() {
  const { id } = useParams();
  const [merchandise, setMerchandise] = useState<Merchandise>();
  const [conflictMerchandise, setConflictMerchandise] = useState<Merchandise>();
  const [conflictDeleted, setConflictDeleted] = useState(false);
  const [editCompleted, setEditCompleted] = useState(false);
  const [cookies, setCookie] = useCookies(['token']);

  useEffect(() => {
    const url = `https://localhost:7190/api/merchandises/${id}`;
    fetch(url, {
      cache: 'no-cache',
      method: 'GET',
      headers: { 'Content-Type': 'application/json' }
    }).then(async response => {
      const responseData = await response.json() as Merchandise;
      setMerchandise(responseData);
    })
  }, [])

  const handleSubmit = async () => {
    setConflictDeleted(false);
    setConflictMerchandise(undefined);

    const url = `https://localhost:7190/api/merchandises/${merchandise?.id}/${merchandise?.version}`;

    const response = await fetch(url, {
      cache: 'no-cache',
      method: 'PUT',
      headers: { 'Content-Type': 'application/json', 'Authorization': `Bearer ${cookies.token}` },
      body: JSON.stringify({
        id: id,
        version: merchandise?.version,
        name: merchandise?.name,
        description: merchandise?.description,
        price: merchandise?.price
      })
    });

    if(response.status === 409) {
        response.json().then(merch => {
            setConflictMerchandise(merch);
            setMerchandise({...merchandise!, version: merch.version})
        })
        .catch(() => {
            setConflictDeleted(true);
        })
    }
    else {
      setMerchandise(await response.json());
      setEditCompleted(true);
    }
  };

  const conflictMessage = conflictDeleted ? "This merchandise has been deleted by another user." : "This merchandise has been updated by another user."

  return (
      <Container maxWidth="xs">
        <CssBaseline />
        <Box
          sx={{
            marginTop: 8,
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
          }}
        >
          {editCompleted && (
            <Alert severity="success" onClose={() => setEditCompleted(false)}>
                <AlertTitle>Success</AlertTitle>
                Merchandise updated successfully!
            </Alert>
          )}
          {(conflictDeleted || conflictMerchandise) && (
            <Alert severity="error">
                <AlertTitle>Conflict</AlertTitle>
                {conflictMessage}
            </Alert>
          )}
          <Typography>{`ID: ${merchandise?.id}`}</Typography>
          <Box sx={{ mt: 1 }}>
            <TextField
            variant='standard'
            margin="normal"
            fullWidth
            label="Name"
            value={merchandise?.name || ""}
            onChange={(event) => setMerchandise({...merchandise!, name: event.target.value})}
            />
            {conflictMerchandise && (
                <span style={{color : "red"}}>{`Current value: ${conflictMerchandise.name}`}</span>
            )}
            <TextField
            variant='standard'
            margin="normal"
            fullWidth
            label="Description"
            value={merchandise?.description || ""}
            onChange={(event) => setMerchandise({...merchandise!, description: event.target.value})}
            />
            {conflictMerchandise && (
                <span style={{color : "red"}}>{`Current value: ${conflictMerchandise.description}`}</span>
            )}
            <TextField
            variant='standard'
            margin="normal"
            fullWidth
            type="number"
            label="Price"
            value={merchandise?.price || 0}
            onChange={(event) => setMerchandise({...merchandise!, price: parseFloat(event.target.value)})}
            />
            {conflictMerchandise && (
                <span style={{color : "red"}}>{`Current value: ${conflictMerchandise.price}`}</span>
            )}
            <Button
            type="submit"
            fullWidth
            variant="contained"
            sx={{ mt: 3, mb: 2 }}
            onClick={handleSubmit}
            >
            Update
            </Button>
          </Box>
        </Box>
      </Container>
  );
}