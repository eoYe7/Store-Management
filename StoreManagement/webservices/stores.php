<?php

$dsn = 'mysql:host=localhost;dbname=users';
$username = 'root';
$password = '';

try {
    $pdo = new PDO($dsn, $username, $password);
    

    $pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

    if (isset($_POST['showallstores'])) {
        $query = $pdo->query("SELECT * FROM stores");
        $rows = $query->fetchAll(PDO::FETCH_ASSOC);

        foreach ($rows as $row) {
            $data = $row['store_code'] . "," . $row['store_name'] . ",". $row['location'] . "#";
            echo $data;
        }
    }

    // Insert User
    else if (isset($_POST['addstore'])) {
        $stmt = $pdo->prepare("INSERT INTO stores(store_code, store_name, location) VALUES (:store_code, :store_name, :location)");
        $stmt->bindParam(':store_code', $_POST['store_code']);
        $stmt->bindParam(':store_name', $_POST['store_name']);
        $stmt->bindParam(':location', $_POST['location']);

        if ($stmt->execute()) {
            echo "Inserted successfully";
        } else {
            echo "Insertion failed";
        }
    }

    // Update User
    else if (isset($_POST['updatestore'])) {
        $stmt = $pdo->prepare("UPDATE stores SET store_name = :store_name, location = :location WHERE store_code = :store_code");
        $stmt->bindParam(':store_name', $_POST['store_name']);
        $stmt->bindParam(':location', $_POST['location']);
        $stmt->bindParam(':store_code', $_POST['store_code']);
        if ($stmt->execute()) {
            echo "Updated successfully";
        } else {
            echo "Update failed";
        }
    }

    // Delete User
    else if (isset($_POST['deletestore'])){
        $stmt = $pdo->prepare("DELETE FROM stores WHERE id = :id");
        $stmt->bindParam(':id', $_POST['id']);

        if ($stmt->execute()) {
            echo "Deleted successfully";
        } else {
            echo "Deletion failed";
        }
    }
} catch (PDOException $e) {
    echo "Connection failed: " . $e->getMessage();
}
?>