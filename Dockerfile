# Use Microsoft aspnet image as our base
FROM microsoft/aspnet

#Copy all files to the /app directory (must contain a project.json)
COPY . /app

# Move to the /app directory of the container
WORKDIR /app

# Restore packages with dnu
RUN ["dnu", "restore"]

# Exposes port 5004 to the world
EXPOSE 5004

# Configure the command to start the container
# In this instance we’re starting the Kestrel web server
ENTRYPOINT ["dnx", ".", "kestrel"]